import React, { useEffect, useState } from 'react';
import API_BASE_URL from '../config';
import './AdminHomePage.css';
import Navbar from '../components/Navbar';

const AdminHomePage = () => {
    const [reservations, setReservations] = useState([]);
    const [users, setUsers] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchReservations = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/Reservation`);
                if (!response.ok) {
                    throw new Error('Failed to fetch reservations');
                }
                const data = await response.json();
                setReservations(data);
            } catch (error) {
                setError(error.message);
            }
        };

        const fetchUsers = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/User`);
                if (!response.ok) {
                    throw new Error('Failed to fetch users');
                }
                const usersData = await response.json();
                setUsers(usersData);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchReservations();
        fetchUsers();
    }, []);

    const getUsername = (userId) => {
        const user = users.find(user => user.userId === userId); 
        return user ? user.userName : 'Unknown User'; 
    };

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div className="admin-home-page">
            <Navbar />
            <h2>All Reservations</h2>
            <table>
                <thead>
                    <tr>
                        <th>User Name</th>
                        <th>Table Number</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {reservations.map((reservation) => (
                        <tr key={reservation.reservationId}>
                            <td>{getUsername(reservation.userId)}</td>
                            <td>{reservation.tableId}</td>
                            <td>
                                {new Date(reservation.dateReservation).toLocaleDateString('en-GB', {
                                    day: '2-digit',
                                    month: '2-digit',
                                    year: 'numeric'
                                })}
                            </td>

                        </tr>
                    ))}
                </tbody>
            </table>

        </div>
    );
};

export default AdminHomePage;
