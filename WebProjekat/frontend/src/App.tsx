import { Routes, Route, Navigate } from "react-router-dom";
import { useAuth } from "./hooks/useAuth";
import { LoginPage } from "./pages/LoginPage";
import { RegisterPage } from "./pages/RegisterPage";
import { HomePage } from "./pages/HomePage";
import { NewTripPage } from "./pages/NewTripPage";
import { EditTripPage } from "./pages/EditTravelPlan";
import { AdminPage } from "./pages/AdminPage";
import { UserRole } from "./enums/UserRole";
import { UserApi } from "./api/users/UserApi";

const usersApi = new UserApi();

export default function App() {
  const { isAuthenticated, user } = useAuth();

  return (
      <Routes>
        <Route path="/" element={<LoginPage usersApi={usersApi} />} />
        <Route path="/register" element={<RegisterPage  usersApi={usersApi}/>} />
        
        <Route path="/home" element={
          isAuthenticated ? <HomePage /> : <Navigate to="/" />
        } />
        <Route path="/trips/new" element={
          isAuthenticated ? <NewTripPage /> : <Navigate to="/" />
        } />
        <Route path="/trips/:id/edit" element={
          isAuthenticated ? <EditTripPage /> : <Navigate to="/" />
        } />
        <Route path="/trips/:id"
          element={isAuthenticated ? <EditTripPage /> : <Navigate to="/" />}
        />
        <Route path="/admin" element={
          isAuthenticated && user?.role === UserRole.ADMIN
            ? <AdminPage />
            : <Navigate to="/" />
        } />
      </Routes>
  );
}