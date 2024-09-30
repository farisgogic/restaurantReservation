
using AutoMapper;
using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using Restaurant_Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public class ReservationService : BaseCRUDService<Restaurant_Model.Reservation, Database.Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>, IReservationService
    {
        public ReservationService(RestaurantDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override IQueryable<Reservation> AddFilter(IQueryable<Reservation> query, ReservationSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (search?.TableId != null)
            {
                filteredQuery = filteredQuery.Where(x => x.TableId == search.TableId);
            }
            if (search?.UserId != null)
            {
                filteredQuery = filteredQuery.Where(x => x.UserId == search.UserId);
            }

            if (search?.DateReservation != null)
            {
                filteredQuery = filteredQuery.Where(x => x.DateReservation.Date == search.DateReservation.Value.Date);
            }

            return filteredQuery;
        }

        public override Restaurant_Model.Reservation Insert(ReservationUpsertRequest insert)
        {
            var userExists = context.Users.Any(u => u.UserId == insert.UserId);
            if (!userExists)
            {
                throw new Exception("User not found.");
            }

            var isReserved = context.Reservations
                .Any(r => r.TableId == insert.TableId && r.DateReservation.Date == insert.DateReservation.Date);

            if (isReserved)
            {
                throw new Exception("Table is already reserved for this date.");
            }

            insert.CreatedAt = DateTime.UtcNow; 

            var reservation = base.Insert(insert);

            var table = context.Tables.FirstOrDefault(t => t.TableId == insert.TableId);
            if (table != null)
            {
                table.isOccupied = true;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Table not found.");
            }

            return reservation;
        }

        public override Restaurant_Model.Reservation Update(int id, ReservationUpsertRequest update)
        {
            var existingReservation = context.Reservations.Find(id);

            if (existingReservation == null)
            {
                throw new Exception("Reservation not found");
            }

            var userExists = context.Users.Any(u => u.UserId == update.UserId);
            if (!userExists)
            {
                throw new Exception("User not found.");
            }

            var isReserved = context.Reservations
                .Any(r => r.TableId == update.TableId && r.DateReservation.Date == update.DateReservation.Date && r.ReservationId != id);

            if (isReserved)
            {
                throw new Exception("Table is already reserved for this date.");
            }

            var reservation = base.Update(id, update);

            var table = context.Tables.FirstOrDefault(t => t.TableId == update.TableId);
            if (table != null)
            {
                table.isOccupied = true;
                context.SaveChanges();
            }

            if (existingReservation.TableId != update.TableId)
            {
                var oldTable = context.Tables.FirstOrDefault(t => t.TableId == existingReservation.TableId);
                if (oldTable != null && !context.Reservations.Any(r => r.TableId == oldTable.TableId && r.DateReservation.Date == existingReservation.DateReservation.Date))
                {
                    oldTable.isOccupied = false;
                    context.SaveChanges();
                }
            }

            return reservation;
        }


        public override void Delete(int id)
        {
            var reservation = context.Reservations.Find(id);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }

            base.Delete(id);

            var hasOtherReservations = context.Reservations
                .Any(r => r.TableId == reservation.TableId && r.DateReservation.Date == reservation.DateReservation.Date);

            if (!hasOtherReservations)
            {
                var table = context.Tables.FirstOrDefault(t => t.TableId == reservation.TableId);
                if (table != null)
                {
                    table.isOccupied = false;
                    context.SaveChanges();
                }
            }
        }
    }
}
