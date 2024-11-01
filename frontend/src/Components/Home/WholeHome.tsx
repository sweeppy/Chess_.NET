import Sidebar from "./Sidebar";
import Welcome from "./Welcome";

const WholeHome = () => {
  return (
    <div className="even-columns no-scroll">
      <Welcome />
      <Sidebar />
    </div>
  );
};

export default WholeHome;
