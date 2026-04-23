import { Routes, Route } from "react-router-dom";
import { LoginPage } from "./pages/LoginPage";
import { RegisterPage } from "./pages/RegisterPage";
import { HomePage } from "./pages/HomePage";
import { Toaster } from "react-hot-toast";
import { UserApi } from "./api/users/UserApi";

const usersApi = new UserApi();

function App() {
  return (
    <>
      <Toaster />

      <Routes>
        <Route path="/" element={<LoginPage usersApi={usersApi} />} />
        <Route path="/register" element={<RegisterPage usersApi={usersApi} />} />
        <Route path="/home" element={<HomePage />} />
      </Routes>
    </>
  );
}

export default App;