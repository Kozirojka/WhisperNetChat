import {useRef, useState, useEffect } from 'react';
import API_BASE_URL from '../../API/base';
const LOGIN_URL = `/login`;
import useAuth from '../../Hooks/useAuth';
import { Link, useNavigate, useLocation } from 'react-router-dom';



const Login = () => {
    const {setAuth} = useAuth();

    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || '/';



    const userRef = useRef();
    const errRef = useRef();

    const [user, setUser] = useState("");
    const [pwd, setPwd] = useState("");
    const [errMsg, setErrMsg] = useState("");


    useEffect(() => {
        userRef.current.focus();
    }, []);

    useEffect(() => {
        setErrMsg("");
    }, [user]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        try{
            const responce = await fetch(`${API_BASE_URL}${LOGIN_URL}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Email: user, 
                    Password: pwd
                }),
            });

            if(!responce.ok) {
                throw new Error('Invalid credentials');
            }

            const data = responce.json();
            console.log(data);

            
            const accessToken = data.token;
            const role = data.role;

            setAuth({user, pwd, role, accessToken});
 
            navigate(from, {replace: true});
            

        }catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Missing Username or Password');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Login Failed');
            }
            errRef.current.focus();
        }
        setUser("");
        setPwd("");
        
    }

    return (
        <>
            <section>
            <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
            <h1>Sign In</h1>

            <form onSubmit={handleSubmit}>
                <label htmlFor="email">
                    Username:
                </label>
                <input
                 type="text"
                id="email"
                ref={userRef}
                autoComplete="off"
                onChange={(e) => setUser(e.target.value)}
                value={user}
                required>
                 
                </input>


                <label htmlFor="password">
                    password:
                </label>
                <input
                 type="text"
                id="password"
                onChange={(e) => setPwd(e.target.value)}
                value={pwd}
                required
                />
            
                <button>Sign In</button>
            </form>
            <p>
                Need an Account?<br />
                <span className="line">
                    <Link to="/register">Sign Up</Link>
                </span>
            </p>
            </section>
        </>
    )
}


export default Login;