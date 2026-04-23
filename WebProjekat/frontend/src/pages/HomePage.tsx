import { HomeContent } from "../components/home/HomeContent";
import { NavBar } from "../components/menu/NavBar";

export function HomePage() {
  return (
    <section className="flex flex-col items-center bg-gradient-to-b from-blue-200 to-white px-4 py-4 min-h-screen">
      <NavBar  />
      <HomeContent />
    </section>
  );
}