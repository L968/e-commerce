import GlobalCss from "./global/global.css";
import Routes from "./Routes";
import Modal from "react-modal";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./global/notify.css";

Modal.setAppElement("#root");

function App() {
  return (
    <div>
      <GlobalCss />
      <ToastContainer />
      <Routes />
    </div>
  );
}

export default App;
