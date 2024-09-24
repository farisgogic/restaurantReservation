import React, { useEffect, useState } from 'react';
import API_BASE_URL from '../config';
import './Reservation.css';
import Navbar from '../components/Navbar';

const ReservationsPage = () => {
    const [reservations, setReservations] = useState([]);
    const [username, setUsername] = useState('');
    const userId = localStorage.getItem('userId');

    useEffect(() => {
        // Fetch reservations for the logged-in user
        const fetchReservations = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/Reservation?UserId=${userId}`);
                const data = await response.json();
                setReservations(data);
            } catch (error) {
                console.error('Error fetching reservations:', error);
            }
        };

        const fetchUserDetails = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/User/${userId}`);
                const userData = await response.json();
                setUsername(userData.userName); 
            } catch (error) {
                console.error('Error fetching user details:', error);
            }
        };

        fetchReservations();
        fetchUserDetails();
    }, [userId]);

    return (
        <div className='reservation'>
            <Navbar />
            <h2>Reservations for User {username}</h2> { }
            <table>
                <thead>
                    <tr>
                        <th>Table Number</th>
                        <th>Date</th>
                        <th>Reservation Created at</th>
                    </tr>
                </thead>
                <tbody>
                    {reservations.map((reservation) => (
                        <tr key={reservation.reservationId}>
                            <td>{reservation.tableId}</td>
                            <td>{new Date(reservation.dateReservation).toLocaleDateString('en-GB', {
                                    year: 'numeric',
                                    month: '2-digit',
                                    day: '2-digit',
                                })}</td>
                            <td>
                                {new Date(reservation.createdAt).toLocaleString('en-GB', {
                                    year: 'numeric',
                                    month: '2-digit',
                                    day: '2-digit',
                                    hour: '2-digit',
                                    minute: '2-digit',
                                    hour12: true 
                                })}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default ReservationsPage;
