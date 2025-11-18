import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Home from "./Home"
import Registration from "./Registration";

function App() {
    const [count, setCount] = useState(0)

    const path = window.location.pathname;

    if (path === "/registration") {
        return (<div><Registration /></div>);
    }

    return (
        <div>
            <h1>Cars</h1>
            <Home />
        </div>
    );
}

export default App
