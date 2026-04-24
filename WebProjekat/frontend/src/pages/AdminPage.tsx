import { AdminPanel } from "../components/user/AdminPanel";
import { UserApi } from "../api/users/UserApi";
import { useAuth } from "../hooks/useAuth";
import { NavBar } from "../components/menu/NavBar";


export function AdminPage() {
  const { token } = useAuth();
  const userApi = new UserApi(token ?? "");

 return (
  <div className="min-h-screen bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] text-white">

    <NavBar />

    <div className="max-w-7xl mx-auto px-6 py-10">
      <AdminPanel userApi={userApi} />
    </div>

  </div>
);
}