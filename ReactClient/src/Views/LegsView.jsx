import { flightsLogsContext } from "Context/flightsLogsContext";
import { Center, Icon, Table } from "UIKit";
import { useContext, useEffect, useState } from "react";
import { NavLink } from "react-router-dom";

export const LegsView = () => {
    const {logs} = useContext(flightsLogsContext);
    const [legsToRender, setLegsToRender] = useState([]);
    
    useEffect(() => {
        console.log("[LegsView] Got new logs: ", logs);
        const legsNum = 8;
        const localLogs = [...logs];
        localLogs.reverse();
        const legsLogs = new Array(legsNum);
        
        for (let i = 0; i < legsLogs.length; i++) {
            legsLogs[i] = localLogs.find((log) => log.nextLegNum === i+1);
            //console.log("legLog ", i+1, ": ", legsLogs[i]);
            if (legsLogs[i] === undefined){
                continue;
            }
            const lastLog = localLogs.find((log) => log.flightNum === legsLogs[i].flightNum && log.legNum === legsLogs[i].nextLegNum);
            if (i === 3 && legsLogs[i].legNum === 8 && lastLog.nextLegNum !== 0){
                continue;
            }
            if (lastLog !== undefined){
                legsLogs[i] = undefined;
            }
        }
        console.log("LegLogs is now: ", legsLogs);
        setLegsToRender(legsLogs);
    }, [logs]);

    return (
        <Table>
            <thead>
                <tr>
                    <th><Center>Leg</Center></th>
                    <th><Center>Current Flight</Center></th>
                    <th><Center>Actions</Center></th>
                </tr>
            </thead>
            <tbody>
                {legsToRender.map((flightLog, index) => 
                    <tr key={index}>
                        <td><Center>{index + 1}</Center></td>
                        <td><Center>{flightLog !== undefined ? flightLog.flightNum.substring(0,8) : "-"}</Center></td>
                        <td>
                            <Center>
                                <NavLink to={`/legs/${index + 1}`}>
                                    <Icon i="info"></Icon>
                                </NavLink>
                            </Center>
                        </td>
                    </tr>
                )}
            </tbody>
        </Table>
    )
}