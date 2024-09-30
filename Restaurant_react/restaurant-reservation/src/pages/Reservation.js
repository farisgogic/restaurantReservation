import React, { useEffect, useState } from 'react';
import API_BASE_URL from '../config';
import './Reservation.css';
import Navbar from '../components/Navbar';

const ReservationsPage = () => {
    const [reservations, setReservations] = useState([]);
    const [username, setUsername] = useState('');
    const [editReservationId, setEditReservationId] = useState(null);
    const [newDate, setNewDate] = useState('');
    const userId = localStorage.getItem('userId');

    useEffect(() => {
        const fetchReservations = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/Reservation?UserId=${userId}`);
                const data = await response.json();
    
                const currentDate = new Date();
                const validReservations = data.filter(reservation => {
                    const reservationDate = new Date(reservation.dateReservation);
                    return reservationDate >= currentDate; 
                });
    
                setReservations(validReservations);
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
    

    const handleDelete = async (reservationId, dateReservation) => {
        const reservationDate = new Date(dateReservation);
        const currentDate = new Date();
        const timeDifference = reservationDate - currentDate;

        if (timeDifference > 24 * 60 * 60 * 1000) {
            try {
                await fetch(`${API_BASE_URL}/Reservation/${reservationId}`, {
                    method: 'DELETE',
                });
                setReservations(reservations.filter(res => res.reservationId !== reservationId));
            } catch (error) {
                console.error('Error deleting reservation:', error);
            }
        } else {
            alert("Reservation can only be deleted if it's more than 24 hours away.");
        }
    };

    const handleEdit = async (reservationId) => {
        if (!newDate) return;
    
        const selectedDate = new Date(newDate);
        const currentDate = new Date();
    
        if (selectedDate < currentDate) {
            alert("The selected date cannot be in the past.");
            return; 
        }
    
        try {
            const existingReservationResponse = await fetch(`${API_BASE_URL}/Reservation/${reservationId}`);
            const existingReservation = await existingReservationResponse.json();
    
            const updatedReservation = {
                userId: existingReservation.userId,
                tableId: existingReservation.tableId,
                dateReservation: newDate,
                createdAt: existingReservation.createdAt 
            };
    
            await fetch(`${API_BASE_URL}/Reservation/${reservationId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedReservation),
            });
    
            const updatedReservations = reservations.map(res => 
                res.reservationId === reservationId ? { ...res, dateReservation: newDate } : res
            );
            setReservations(updatedReservations);
            setEditReservationId(null);
        } catch (error) {
            console.error('Error updating reservation:', error);
        }
    };
    
    

    return (
        <div className='reservation'>
            <Navbar />
            <h2>Reservations for User {username}</h2>
            <table>
                <thead>
                    <tr>
                        <th>Table Number</th>
                        <th>Date</th>
                        <th>Reservation Created at</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {reservations.map((reservation) => (
                        <tr key={reservation.reservationId}>
                            <td>{reservation.tableId}</td>
                            <td>
                                {editReservationId === reservation.reservationId ? (
                                    <input
                                        type="date"
                                        value={newDate}
                                        onChange={(e) => setNewDate(e.target.value)}
                                    />
                                ) : (
                                    new Date(reservation.dateReservation).toLocaleDateString('en-GB', {
                                        year: 'numeric',
                                        month: '2-digit',
                                        day: '2-digit',
                                    })
                                )}
                            </td>
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
                            <td>
                                {editReservationId === reservation.reservationId ? (
                                    <button onClick={() => handleEdit(reservation.reservationId)}>Save</button>
                                ) : (
                                    <button onClick={() => setEditReservationId(reservation.reservationId)}>Edit</button>
                                )}
                            </td>
                            <td>
                                <button onClick={() => handleDelete(reservation.reservationId, reservation.dateReservation)}>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default ReservationsPage;
