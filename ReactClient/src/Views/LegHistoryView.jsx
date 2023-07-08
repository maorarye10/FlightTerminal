import { flightsLogsContext } from "Context/flightsLogsContext"
import { Btn, Center, Rows, Table } from "UIKit";
import { useContext, useEffect, useState } from "react"
import { useNavigate } from "react-router-dom";

export const LegHistoryView = () => {
    const {logs} = useContext(flightsLogsContext);
    const [logsToRender, setLogsToRender] = useState([]);
    const pathNameSplits = window.location.pathname.split('/');
    const legNum = pathNameSplits[pathNameSplits.length - 1];
    const navigate = useNavigate();

    useEffect(() => {
        const localLogs = [...logs];
        const filteredLogs = localLogs.filter((log) => log.legNum === Number(legNum)).reverse(); 
        setLogsToRender(filteredLogs);
    }, [logs]);

    const handleClick = () => {
        navigate('/legs');
    }

    return (
        <Rows>
            <Table>
                <thead>
                    <tr>
                        <th><Center>Flight</Center></th>
                        <th><Center>Passengers</Center></th>
                        <th><Center>Brand</Center></th>
                        <th><Center>Leg</Center></th>
                        <th><Center>In</Center></th>
                        <th><Center>Out</Center></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        logsToRender.length === 0
                        ?
                        <tr>
                            <td></td>
                            <td></td>
                            <td colSpan={2}><Center>No flights to show</Center></td>
                            <td></td>
                            <td></td>
                        </tr>
                        :
                        logsToRender.map(log => 
                            <tr key={log.id}>
                                <td><Center>{log.flightNum.substring(0,8)}</Center></td>
                                <td><Center>{log.passengersCount}</Center></td>
                                <td><Center>{log.brandName}</Center></td>
                                <td><Center>{log.legNum}</Center></td>
                                <td><Center>{log.in}</Center></td>
                                <td><Center>{log.out}</Center></td>
                            </tr>
                        )
                    }
                </tbody>
            </Table>
            <Center><Btn i="arrow_back" onClick={handleClick}>Go Back</Btn></Center>
        </Rows>
    )
}