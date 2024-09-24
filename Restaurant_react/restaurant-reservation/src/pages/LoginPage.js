import React, { useState } from 'react';  
import { useNavigate } from 'react-router-dom';
import './LoginPage.css'; 
import API_BASE_URL from '../config';
import { jwtDecode } from 'jwt-decode'; 

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState(''); 
    const [usernameError, setUsernameError] = useState(''); 
    const [passwordError, setPasswordError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
    
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
    
        const response = await fetch(`${API_BASE_URL}/User/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ username, password }),
        });
    
        if (response.ok) {
            const data = await response.json();
            const token = data.token; 
    
            const decoded = jwtDecode(token);
            console.log('User ID:', decoded.sub); 
            console.log('Name:', decoded.unique_name);
            console.log('Role:', decoded.role);
    
            localStorage.setItem('userId', decoded.sub);
            localStorage.setItem('userRole', decoded.role);
    
            if (decoded.role === 'admin') {
                navigate('/admin-home'); 
            } else if (decoded.role === 'korisnik') {
                navigate('/tables'); 
            } else {
                setErrorMessage('Role not recognized.');
            }
        } else {
            setErrorMessage('Invalid username or password. Please try again.');
        }
    };
    
    

    const handleRegister = () => {
        navigate('/register'); 
    };

    return (
        <div className="login-wrapper">
            <div className="login-container">
                <h2>Login</h2>
                <form onSubmit={handleLogin}>
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
                    <button type="submit">Login</button>
                    {errorMessage && <div className="error">{errorMessage}</div>}
                </form>
                <button onClick={handleRegister} className="register-button">
                    Register
                </button>
            </div>
        </div>
    );
    
};

export default LoginPage;
