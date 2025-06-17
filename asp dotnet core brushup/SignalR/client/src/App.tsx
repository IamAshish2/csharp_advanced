import { Route, Routes } from "react-router-dom";
import SignInPage from "./pages/auth/signin";
import { Toaster } from "sonner";
import ChatRoom from "./pages/layout/private/chat-room/chat-room";
import WaitingRoom from "./pages/layout/private/waiting-room/waiting-room";

const App = () => {

  return (
    <>
      <Toaster />
      
      {/* public routes */}
      <Routes>
        <Route index element={<SignInPage />} />
      </Routes>

      {/* private routes */}
      <Routes>
        <Route path="user-chats" element={<ChatRoom />} />
        <Route path="waiting-room" element={<WaitingRoom />} />
      </Routes>
    </>

  );
};

export default App;
