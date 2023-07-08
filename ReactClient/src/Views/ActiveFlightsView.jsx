import { flightsLogsContext } from "Context/flightsLogsContext";
import { Center, Icon, Table } from "UIKit"
import { useContext, useEffect, useState } from "react";
import { NavLink } from "react-router-dom";

export const ActiveFlightsView = () => {
    const {logs} = useContext(flightsLogsContext);
    const [flightsToRender, setFlightsToRender] = useState([]);

    useEffect(() => {
        const localLogs = [...logs];
        console.log("Logs are now: ", localLogs);
        let flights = localLogs.filter((log) => log.legNum === 0).map((log) => {
            return {
                id: log.id,
                flightNum: log.flightNum,
                legNum: log.legNum
            }
        });
        // sorts the logs by flight number & times
        localLogs.sort((a, b) => {
            const compareRes = a.flightNum.localeCompare(b.flightNum);
            if (compareRes === 0){
                return Date.parse(a.out) - Date.parse(b.out);
            }
            return compareRes;
        });

        //console.log("Flights: ", flights);
        //console.log("Ordered Logs: ", orderedLogs);

        flights.forEach(flight => {
            const flightLogs = localLogs.filter((log) => log.flightNum === flight.flightNum);
            //console.log("Flight ", flight.flightNum, " logs: ", flightLogs);
            if (flightLogs.length === 9){
                flight.legNum = -1;
            }
            else{
                flight.legNum = flightLogs[flightLogs.length - 1].nextLegNum;
            }
        });
        
        if (flights && flights.length > 0){
            flights = flights.filter((flight) => flight.legNum !== -1);
        }
        setFlightsToRender(flights);
        //console.log("Flights after change: ", flights);
    }, [logs]);

    

    return (
        <Table>
            <thead>
                <tr>
                    <th><Center>Flight</Center></th>
                    <th><Center>Current Leg</Center></th>
                    <th><Center>Actions</Center></th>
                </tr>
            </thead>
            <tbody>
                {
                    flightsToRender.length === 0
                    ?
                    <tr>
                        <td></td>
                        <td><Center>No active flights to show</Center></td>
                        <td></td>
                        {/* {console.log(Date.parse(log.out))} */}
                    </tr>
                    :
                    flightsToRender.reverse().map(log => 
                        <tr key={log.id}>
                            <td><Center>{log.flightNum.substring(0,8)}</Center></td>
                            <td><Center>{log.legNum}</Center></td>
                            <td>
                                <Center>
                                    <NavLink to={`/flights/${log.flightNum}`}>
                                        <Icon i="info"></Icon>
                                    </NavLink>
                                </Center>
                            </td>
                            {/* {console.log(Date.parse(log.out))} */}
                        </tr>
                    )
                }
            </tbody>
        </Table>
    )
    
}