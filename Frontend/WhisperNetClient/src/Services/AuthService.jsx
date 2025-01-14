const BASE_URL = 'https://localhost:7271';

export const authService = {
    async login(email, password)  {
        const responce = await fetch(`${BASE_URL}/login`, {
            method: 'Post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password }),
    });


    if(!responce.ok) {
        throw new Error('Invalid credentials');
    }

    return responce.json();
    }
    
}