import { useEffect, useState } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

export default function Registration() {
    const [cars, setCars] = useState([]);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5146/carStatusHub")
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

        connection.start()
            .then(() => console.log("Connected to SignalR"))
            .catch(err => console.error("Connection failed: ", err));
        connection.on("RegistrationStatusUpdated", (data) => {
            setCars(data);
        });

        return () => connection.stop();
    }, []);

    return (
        <div>
            <h2>Registration Status (Live)</h2>

            <table border="1" cellPadding="8">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Make</th>
                        <th>Owner</th>
                        <th>License</th>
                        <th>Expiry</th>
                        <th>Status</th>
                    </tr>
                </thead>

                <tbody>
                    {cars.map((c) => (
                        <tr key={c.id}>
                            <td>{c.id}</td>
                            <td>{c.make}</td>
                            <td>{c.owner}</td>
                            <td>{c.licenseNumber}</td>
                            <td>{new Date(c.registrationExpiry).toLocaleString()}</td>
                            <td style={{ color: c.isExpired ? "red" : "green" }}>
                                {c.isExpired ? "Expired" : "Valid"}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
