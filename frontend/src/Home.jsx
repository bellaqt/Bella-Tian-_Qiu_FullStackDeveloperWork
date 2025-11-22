import { API_BASE_URL } from "./config";
import { useEffect, useState } from "react";

function Cars() {
    const [cars, setCars] = useState([]);

    useEffect(() => {
        fetch(`${API_BASE_URL}/cars`)
            .then(res => res.json())
            .then(setCars)
            .catch(err => console.error("Fetch error:", err));
    }, []);

    return (
        <div>
            {cars.map(c => (
                <p key={c.id}>{c.make} - {c.owner}</p>
            ))}
        </div>
    );
}

export default Cars;