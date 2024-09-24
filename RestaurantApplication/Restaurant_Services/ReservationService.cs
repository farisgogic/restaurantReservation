
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

            if(search?.TableId != null)
            {
                filteredQuery = filteredQuery.Where(x=>x.TableId == search.TableId);
            }
            if(search?.UserId != null)
            {
                filteredQuery = filteredQuery.Where(x=>x.UserId == search.UserId);
            }

            if (search?.DateReservation != null)
            {
                filteredQuery = filteredQuery.Where(x => x.DateReservation.Date == search.DateReservation.Value.Date);
            }

            return filteredQuery;
        }

        public override Restaurant_Model.Reservation Insert(ReservationUpsertRequest insert)
        {
            // Check if the table is already reserved on the requested date
            var isReserved = context.Reservations
                .Any(r => r.TableId == insert.TableId && r.DateReservation.Date == insert.DateReservation.Date);

            if (isReserved)
            {
                throw new Exception("Table is already reserved for this date.");
            }

            insert.CreatedAt = DateTime.UtcNow; // Set the creation date

            // Call the base method to insert the reservation
            var reservation = base.Insert(insert);

            // Update the table to set isOccupied to true
            var table = context.Tables.FirstOrDefault(t => t.TableId == insert.TableId);
            if (table != null)
            {
                table.isOccupied = true; // Set the table as occupied
                context.SaveChanges(); // Save changes to the database

            }
            else
            {
                throw new Exception("Table not found.");
            }

            return reservation;
        }

    }
}
