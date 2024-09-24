import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './RegistrationPage.css'; 
import API_BASE_URL from '../config';

const RegistrationPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState(''); 
    const [usernameError, setUsernameError] = useState(''); 
    const [passwordError, setPasswordError] = useState(''); 
    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();

        // Reset errors
        setErrorMessage('');
        setUsernameError('');
        setPasswordError('');

        if (!username) {
            setUsernameError('Username is required.');
        }
        if (!password) {
            setPasswordError('Password is required.');
        }

        if (!username || !password) {
            return;
        }

        const roleId = 2;

        const response = await fetch(`${API_BASE_URL}/User`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ username, password, roleId }), 
        });

        if (response.ok) {
            navigate('/login');
        } else {
            const data = await response.json();
            setErrorMessage(data.message || 'Registration failed. Please try again.'); 
        }
    };

    return (
        <div className="registration-wrapper">
            <div className="registration-container">
                <h2>Register</h2>
                <form onSubmit={handleRegister}>
                    <div className="form-group">
                        <label>Username</label>
                        <input
                            type="text"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />
                        {usernameError && <div className="error">{usernameError}</div>}
                    </div>
                    <div className="form-group">
                        <label>Password</label>
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        {passwordError && <div className="error">{passwordError}</div>}
                    </div>
                    <button type="submit">Register</button>
                    {errorMessage && <div className="error">{errorMessage}</div>}
                </form>
                <div className="login-prompt">
                    Already have an account? 
                    <span 
                        className="login-link" 
                        onClick={() => navigate('/login')}
                    >
                        Login
                    </span>
                </div>
            </div>
        </div>
    );
};

export default RegistrationPage;