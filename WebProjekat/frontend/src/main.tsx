import { createRoot } from 'react-dom/client'
import './index.css'
import { BrowserRouter } from "react-router-dom";
import App from './App.tsx'
import { AuthProvider } from './context/AuthContext.tsx';
import { Toaster } from "react-hot-toast";


createRoot(document.getElementById('root')!).render(
 <BrowserRouter>
    <AuthProvider>
      <App />
      <Toaster position="top-right" />
    </AuthProvider>
  </BrowserRouter>
)
