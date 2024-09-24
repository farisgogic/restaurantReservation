import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import TablesPage from './pages/TablesPage';
import RegistrationPage from './pages/Register';
import ReservationsPage from './pages/Reservation';
import AdminHomePage from './pages/AdminHomePage';
import AdminNewTable from './pages/AdminNewTable';

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Navigate to="/login" />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegistrationPage />} />
                <Route path="/tables" element={<TablesPage />} />
                <Route path="/reservations" element={<ReservationsPage />} />
                <Route path="/admin-home" element={<AdminHomePage />} />
                <Route path="/new-table" element={<AdminNewTable />} />
            </Routes>
        </Router>
    );
};

export default App;
