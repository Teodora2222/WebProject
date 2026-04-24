import { useNavigate } from "react-router-dom";

export function NotFoundPage(){
    const navigate = useNavigate();
    const ToPreviousPage=()=>{
        navigate(-1)
    }
     return (
    <main className="min-h-screen bg-gradient-to-tr from-slate-600/75 to-red-800/70 flex items-center justify-center">
        <div className="bg-white/30 backdrop-blur-lg shadow-lg border border-red-300 rounded-2xl p-10 w-full max-w-lg text-center">
          <h2 className="text-3xl font-bold text-red-800/70 mb-4">
            404 Not Found
          </h2>
          <p className="text-gray-800 text-lg mb-6">
            This link is not valid.Please try again with valid URL!
          </p>
          <button
            onClick={ToPreviousPage}
            className="bg-red-700/60! hover:bg-red-700/70! text-white! px-6 py-2 rounded-xl transition"
          >
           Go back to previous Page
          </button>
        </div>
      </main>
    );
}