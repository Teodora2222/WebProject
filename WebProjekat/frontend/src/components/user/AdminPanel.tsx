import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import type { UserDto } from "../../models/users/UserDto";
import type { IUserApi } from "../../api/users/IUserApi";
import { UserRole } from "../../enums/UserRole";

interface Props {
  userApi: IUserApi;
}

export function AdminPanel({ userApi }: Props) {
  const [users, setUsers] = useState<UserDto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    userApi.getAll()
      .then(setUsers)
      .catch(() => toast.error("Failed to load users."))
      .finally(() => setLoading(false));
  }, []);

  const handleDelete = async (id: number) => {
    if (!confirm("Delete this user?")) return;
    try {
      await userApi.deleteUserById(id);
      setUsers((prev) => prev.filter((u) => u.id !== id));
      toast.success("User deleted.");
    } catch {
      toast.error("Failed to delete user.");
    }
  };
return (
  <div className="min-h-screen  text-white px-6 py-10">

    <div className="w-full">

      <div className="mb-8">
        <h1 className="text-3xl md:text-4xl font-bold">
          Admin Panel ⚙️
        </h1>
        <p className="text-gray-300 mt-2">
          {users.length} registered users
        </p>
      </div>

      {loading && (
        <p className="text-gray-400 text-center mt-20 animate-pulse">
          Loading users...
        </p>
      )}

      {!loading && (
        <div className="bg-white/10 backdrop-blur-xl border border-white/20 rounded-3xl shadow-xl overflow-hidden">

          <div className="grid grid-cols-4 px-6 py-4 text-sm text-gray-300 border-b border-white/10">
            <span>Name</span>
            <span>Email</span>
            <span>Role</span>
            <span className="text-right">Actions</span>
          </div>

          <div className="divide-y divide-white/10 max-h-[500px] overflow-y-auto">

            {users.map((u) => (
              <div
                key={u.id}
                className="grid grid-cols-4 items-center px-6 py-4 hover:bg-white/5 transition group"
              >

                <div className="font-medium">
                  {u.firstName} {u.lastName}
                </div>

                <div className="text-gray-300 text-sm">
                  {u.email}
                </div>

                <div>
                  <span
                    className={`px-3 py-1 rounded-full text-xs font-semibold ${
                      u.role === UserRole.ADMIN
                        ? "bg-emerald-500/20 text-emerald-400 border border-emerald-400/30"
                        : "bg-gray-700/40 text-gray-300 border border-white/10"
                    }`}
                  >
                    {u.role}
                  </span>
                </div>

                <div className="flex justify-end">

                  <button
                    onClick={() => u.id && handleDelete(u.id)}
                    className="px-3 py-1 rounded-lg text-sm text-red-400 hover:bg-red-500/10 hover:text-red-300 transition"
                  >
                    Delete
                  </button>

                </div>

              </div>
            ))}

          </div>
        </div>
      )}
    </div>
  </div>
);
}