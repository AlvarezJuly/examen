import React from "react";
import './App.css';
import CrudCliente from "./Components/CrudCliente";


function App() {
return(
  
  <div className="App">
    <h1> Tienda JJ </h1>
    <CrudCliente /> {/*llamado del componente*/}
  </div>
);
}
export default App;
