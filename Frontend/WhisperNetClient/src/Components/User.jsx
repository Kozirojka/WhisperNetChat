import {useEffect, useState} from 'react';
import useFetchPrivate from "../Hooks/useFetchPrivate";
import { useNavigate, useLocation } from "react-router-dom";

const Users = () => {
    const [users, setUsers] = useState([]);
    const fetchPrivate = useFetchPrivate();
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        let isMounted = true;
        const controller = new AbortController();

        const getUsers = async () => {
            try {
                const response = await fetchPrivate("/users", {
                    method: 'GET',
                    signal: controller.signal,
                });

                if (!response.ok) {
                    if (response.status === 401 || response.status === 403) {
                        navigate("/login", { state: { from: location }, replace: true });
                    }
                    throw new Error(`Error ${response.status}: ${response.statusText}`);
                }

                const data = await response.json();
                console.log(data);
                isMounted && setUsers(data);

            } catch (err) {
                console.error("Fetch users error:", err);
            }
        };

        getUsers();

        return () => {
            isMounted = false;
            controller.abort(); 
        };
    }, [fetchPrivate, navigate, location]);

    return (
        <article>
            <h2>Users List</h2>
            {users?.length
                ? (
                    <ul>
                        {users.map((user, i) => <li key={i}>{user?.username}</li>)}
                    </ul>
                ) : <p>No users to display</p>
            }
        </article>
    );
};

export default Users;