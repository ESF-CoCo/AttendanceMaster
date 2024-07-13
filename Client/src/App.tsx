import { BrowserRouter, Routes, Route } from "react-router-dom";

import Navbar from './components/Navbar';
import Alert from "./components/Alert";
import Home from './pages/Home'
import Login from './pages/Login';
import { useState } from "react";

function App() {
  const [alertOpacity, setAlertOpacity] = useState(0)

  return (
    <>
      <Navbar navItems={['Home']} navRedirect={['/']}/>

      <Alert styling={{opacity: alertOpacity}} type="danger">Text</Alert>

      <BrowserRouter>
        <Routes>
          <Route index element={<Home/>}/>
          <Route path="/login" element={<Login/>}/>
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
