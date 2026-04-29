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
import { NewActivityPage } from "./pages/NewActivityPage";
import { EditActivityPage } from "./pages/EditActivityPage";
import { EditDestinationPage } from "./pages/EditDestinationPage";
import { NewDestinationPage } from "./pages/NewDestinationPage";
import { TripDetailPage } from "./pages/TripDeatilPage";
import { NewExpensePage } from "./pages/NewExpensePage";
import { EditExpensePage } from "./pages/EditExpensePage";
import { SharedPlanPage } from "./pages/SharedPlanPage";

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
       
        <Route path="/admin" element={
          isAuthenticated && user?.role === UserRole.ADMIN
            ? <AdminPage />
            : <Navigate to="/" />
        } />

        <Route path="/trips/:id" element={isAuthenticated ? <TripDetailPage /> : <Navigate to="/" />} />
        <Route path="/trips/:id/destinations/new" element={isAuthenticated ? <NewDestinationPage /> : <Navigate to="/" />} />
        <Route path="/trips/:id/destinations/:destId/edit" element={isAuthenticated ? <EditDestinationPage /> : <Navigate to="/" />} />
        <Route path="/trips/:id/activities/new" element={isAuthenticated ? <NewActivityPage /> : <Navigate to="/" />} />
        <Route path="/trips/:id/activities/:actId/edit" element={isAuthenticated ? <EditActivityPage /> : <Navigate to="/" />} />
        <Route path="/trips/:id/expenses/new" element={
            isAuthenticated ? <NewExpensePage /> : <Navigate to="/" />
        } />
        
      <Route path="/trips/:id/expenses/:expId/edit" element={
        isAuthenticated ? <EditExpensePage /> : <Navigate to="/" />
      } />
      <Route path="/shared/:token" element={<SharedPlanPage />} />
      
      </Routes>
  );
}