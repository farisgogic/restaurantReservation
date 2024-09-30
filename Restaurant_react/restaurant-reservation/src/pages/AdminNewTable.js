import React, { useState, useEffect } from 'react';
import API_BASE_URL from '../config';
import './AdminNewTable.css';
import Navbar from '../components/Navbar';

const AdminNewTable = () => {
    const [tables, setTables] = useState([]); 
    const [showForm, setShowForm] = useState(false); 
    const [tableNumber, setTableNumber] = useState('');
    const [capacity, setCapacity] = useState('');
    const [isOccupied, setIsOccupied] = useState(false);
    const [errorMessage, setErrorMessage] = useState('');
    const [editTableId, setEditTableId] = useState(null); // Dodano za praćenje uređivanja
    const [newCapacity, setNewCapacity] = useState(''); // Dodano za novu vrijednost kapaciteta

    useEffect(() => {
        const fetchTables = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/Table`);
                const data = await response.json();
                setTables(data);
            } catch (error) {
                console.error('Error fetching tables:', error);
            }
        };

        fetchTables();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const tableNum = parseInt(tableNumber);
        const cap = parseInt(capacity);
        
        if (!tableNumber || !capacity) {
            setErrorMessage('All fields are required.');
            return;
        }

        if (tableNum < 0) {
            setErrorMessage('Table number must be greater than or equal to 0.');
            return;
        }

        const existingTable = tables.find(table => table.tableNumber === tableNum);
        if (existingTable) {
            setErrorMessage('Table number already exists. Please choose a different number.');
            return;
        }

        const tableData = {
            tableNumber: tableNum,
            capacity: cap,
            isOccupied: isOccupied,
        };

        try {
            const response = await fetch(`${API_BASE_URL}/Table`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(tableData),
            });

            if (!response.ok) {
                throw new Error('Failed to add new table');
            }

            const updatedResponse = await fetch(`${API_BASE_URL}/Table`);
            const updatedData = await updatedResponse.json();
            setTables(updatedData);

            setTableNumber('');
            setCapacity('');
            setIsOccupied(false);
            setShowForm(false);
        } catch (error) {
            setErrorMessage(error.message);
        }
    };

    // Funkcija za uređivanje kapaciteta stola
    const handleEdit = async (tableId) => {
        if (!newCapacity) return;

        try {
            const response = await fetch(`${API_BASE_URL}/Table/${tableId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ capacity: newCapacity }),
            });

            if (!response.ok) {
                throw new Error('Failed to update table capacity');
            }

            const updatedTables = tables.map(table => 
                table.tableNumber === tableId ? { ...table, capacity: newCapacity } : table
            );
            setTables(updatedTables);
            setEditTableId(null); // Izađi iz moda uređivanja
        } catch (error) {
            console.error('Error updating table capacity:', error);
        }
    };

    // Funkcija za brisanje stola
    const handleDelete = async (tableId) => {
        try {
            await fetch(`${API_BASE_URL}/Table/${tableId}`, {
                method: 'DELETE',
            });

            setTables(tables.filter(table => table.tableNumber !== tableId));
        } catch (error) {
            console.error('Error deleting table:', error);
        }
    };

    return (
        <div className="admin-new-table">
            <Navbar />
            <h2>All Tables</h2>
            <table>
                <thead>
                    <tr>
                        <th>Table Number</th>
                        <th>Capacity</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {tables.map((table) => (
                        <tr key={table.tableNumber}>
                            <td>{table.tableNumber}</td>
                            <td>
                                {editTableId === table.tableNumber ? (
                                    <input
                                        type="number"
                                        value={newCapacity}
                                        onChange={(e) => setNewCapacity(e.target.value)}
                                    />
                                ) : (
                                    table.capacity
                                )}
                            </td>
                            <td>
                                {editTableId === table.tableNumber ? (
                                    <button onClick={() => handleEdit(table.tableNumber)}>Save</button>
                                ) : (
                                    <button onClick={() => setEditTableId(table.tableNumber)}>Edit</button>
                                )}
                            </td>
                            <td>
                                <button onClick={() => handleDelete(table.tableNumber)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <button onClick={() => setShowForm(!showForm)}>
                {showForm ? 'Cancel' : 'Add New Table'}
            </button>

            {showForm && (
                <form onSubmit={handleSubmit} className="new-table-form">
                    <h2>Add New Table</h2>
                    <div className="form-group">
                        <label>Table Number:</label>
                        <input
                            type="number"
                            value={tableNumber}
                            onChange={(e) => setTableNumber(e.target.value)}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Capacity:</label>
                        <input
                            type="number"
                            value={capacity}
                            onChange={(e) => setCapacity(e.target.value)}
                            required
                        />
                    </div>

                    {errorMessage && <p className="error-message">{errorMessage}</p>}

                    <button type="submit" className="submit-button">Add Table</button>
                </form>
            )}
        </div>
    );
};

export default AdminNewTable;
