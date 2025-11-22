import { useEffect, useState } from "react";
import { API_BASE_URL } from "./config";

export default function Home() {
    const [cars, setCars] = useState([]);
    const [selectedMake, setSelectedMake] = useState("");

    useEffect(() => {
        fetch("${API_BASE_URL}/cars")
            .then(res => res.json())
            .then(setCars);
    }, []);

    const makes = [...new Set(cars.map(c => c.make))];

    const filtered = selectedMake
        ? cars.filter(c => c.make === selectedMake)
        : cars;

    return (
        <div>
            <select value={selectedMake} onChange={(e) => setSelectedMake(e.target.value)}>
                <option value="">All</option>
                {makes.map((m) => (
                    <option key={m} value={m}>{m}</option>
                ))}
            </select>

            <table border="1" style={{ marginTop: "20px" }}>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Make</th>
                        <th>Owner</th>
                    </tr>
                </thead>
                <tbody>
                    {filtered.map((c) => (
                        <tr key={c.id}>
                            <td>{c.id}</td>
                            <td>{c.make}</td>
                            <td>{c.owner}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
