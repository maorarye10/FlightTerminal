import { NavLink, Route, Routes } from 'react-router-dom';
import { Center, GridMain, Line, Rows } from 'UIKit';
import './App.css';
import { FlightView } from 'Views/FlightView';
import { ActiveFlightsView } from 'Views/ActiveFlightsView';
import { LegsView } from 'Views/LegsView';
import { useContext, useEffect } from 'react';
import { flightsLogsContext } from 'Context/flightsLogsContext';
import { useSignalR } from 'Hooks/useSignalR';
import { AllFlightsView } from 'Views/AllFlightsView';
import { LegHistoryView } from 'Views/LegHistoryView';

function App() {
  console.log("Rendering App...")
  const [ isConnected, log ] = useSignalR('https://localhost:7014/hubs/flights-logs-hub', 'ReciveFlightLog');
  const { handleNewLog } = useContext(flightsLogsContext);

  useEffect(() => {
    if (isConnected && log){
      handleNewLog(log);
    }
  }, [log]);

  return (
    <div className='app'>
      <GridMain>
        <div>
          {/* <Line>
            <NavLink to='/'>Flights</NavLink>
          </Line> */}
          <Center>
            <Rows>
              <Center><h1>Flights Updates</h1></Center>
              <Center>
                <Line>
                  <NavLink to="/active-flights">Active Flight</NavLink>
                  <NavLink to="/flights">All Flight</NavLink>
                  <NavLink to="/legs">Legs View</NavLink>
                </Line>
              </Center>
            </Rows>
          </Center>
        </div>
        <div>
          <Routes>
            <Route path='/active-flights' element={<ActiveFlightsView />} />
            <Route path='/flights' element={<AllFlightsView />} />
            <Route path='/flights/:id' element={<FlightView />} />
            <Route path='/legs' element={<LegsView />} />
            <Route path='/legs/:id' element={<LegHistoryView />} />
          </Routes>
        </div>
        <div>

        </div>
      </GridMain>
    </div>
  );
}

export default App;
