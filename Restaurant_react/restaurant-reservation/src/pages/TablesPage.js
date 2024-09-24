import React, { useEffect, useState } from 'react';
import { fetchTables } from '../api';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import './TablesPage.css';
import API_BASE_URL from '../config';
import Navbar from '../components/Navbar'

const TablesPage = () => {
    const [tables, setTables] = useState([]);
    const [reservations, setReservations] = useState([]);
    const [error, setError] = useState(null);
    const [selectedDate, setSelectedDate] = useState(new Date());
    const userId = localStorage.getItem('userId');

    useEffect(() => {
        const getTables = async () => {
            try {
                const data = await fetchTables();
                setTables(data);
            } catch (error) {
                setError('Failed to fetch tables');
            }
        };

        getTables();
    }, []);

    const fetchAvailableTables = async () => {
        try {
            const reservationResponse = await fetch(`${API_BASE_URL}/Reservation?DateReservation=${selectedDate.toISOString()}`);
            if (!reservationResponse.ok) {
                throw new Error('Failed to fetch reservations for the selected date');
            }
            const reservationsData = await reservationResponse.json();
            setReservations(reservationsData);

            const tableResponse = await fetchTables();
            const updatedTables = tableResponse.map(table => {
                const isReserved = reservationsData.some(reservation => reservation.tableId === table.tableId);
                return {
                    ...table,
                    isOccupied: isReserved
                };
            });
            setTables(updatedTables);
        } catch (error) {
            setError(error.message);
        }
    };

    const handleReserve = async (tableId) => {
        try {
            const reservationResponse = await fetch(`${API_BASE_URL}/Reservation`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userId: userId,
                    tableId: tableId,
                    dateReservation: selectedDate,
                }),
            });

            if (!reservationResponse.ok) {
                const message = await reservationResponse.text();
                throw new Error(message);
            }

            alert('Table reserved successfully!');
            fetchAvailableTables();
        } catch (error) {
            alert(error.message);
        }
    };

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <Navbar />
            <div className='date'>
                <DatePicker selected={selectedDate} onChange={(date) => setSelectedDate(date)} />
                <button className='check' onClick={fetchAvailableTables}>Check Availability</button>
            </div>

            <table>
                <thead>
                    <tr>
                        <th>Table Number</th>
                        <th>Capacity</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {tables.map((table) => (
                        <tr key={table.tableId}>
                            <td>{table.tableNumber}</td>
                            <td>{table.capacity}</td>
                            <td>{table.isOccupied ? 'Reserved' : 'Available'}</td>
                            <td>
                                <button
                                    onClick={() => handleReserve(table.tableId)}
                                    disabled={table.isOccupied}
                                    className={table.isOccupied ? 'reserved-button' : 'available-button'}
                                >
                                    {table.isOccupied ? 'Reserved' : 'Reserve'}
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default TablesPage;
