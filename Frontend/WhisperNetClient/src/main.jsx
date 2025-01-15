import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import { AuthProvider } from './Context/AuthProvider';
import {  BrowserRouter, Routes, Route} from 'react-router-dom';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>

    <AuthProvider>
      <Routes>
        <Route path="/*" element={<App/>}></Route>
      </Routes>
    </AuthProvider>
    </BrowserRouter>
  </StrictMode>,
)
