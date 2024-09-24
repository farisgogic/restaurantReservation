import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';

const Navbar = () => {
    const navigate = useNavigate();
    const userRole = localStorage.getItem('userRole');

    const handleLogout = () => {
        localStorage.removeItem('userId');
        localStorage.removeItem('userRole'); 
        navigate('/login');
    };

    return (
        <nav className="navbar">
            <ul className="navbar-links">
                {userRole === 'admin' ? (
                    <>
                        <li>
                            <Link to="/admin-home">Admin Home</Link>
                        </li>
                        <li>
                            <Link to="/new-table">Tables</Link>
                        </li>
                        <li>
                            <button onClick={handleLogout} className="logout-button">
                                Logout
                            </button>
                        </li>
                    </>
                ) : (
                    <>
                        <li>
                            <Link to="/tables">Reserve Table</Link>
                        </li>
                        <li>
                            <Link to="/reservations">My Reservations</Link>
                        </li>
                        <li>
                            <button onClick={handleLogout} className="logout-button">
                                Logout
                            </button>
                        </li>
                    </>
                )}
            </ul>
        </nav>
    );
};

export default Navbar;
