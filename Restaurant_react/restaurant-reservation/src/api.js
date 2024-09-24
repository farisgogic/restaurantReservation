const BASE_URL = 'https://localhost:7179/api';

export const fetchTables = async () => {
    const response = await fetch(`${BASE_URL}/Table`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    });

    if (!response.ok) {
        throw new Error('Failed to fetch tables');
    }

    return response.json();
};
