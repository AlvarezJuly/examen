import React, { useEffect, useState } from "react";
import axios from "axios";
import { Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import "../App.css";
import "bootstrap/dist/css/bootstrap.min.css";

const CrudCliente = () => {
  const baseUrl = "https://localhost:44386/api/Clientes";
  const [data, setData] = useState([]);
  const [modalInsertar, setModalInsertar] = useState(false);
  const [modalEditar, setModalEditar] = useState(false);
  const [clienteSeleccionado, setClienteSeleccionado] = useState({
    id: "",
    nombre: "",
    direccion: "",
    telefono: "",
  });
  const [clienteEditar, setClienteEditar] = useState({
    id: "",
    nombre: "",
    direccion: "",
    telefono: "",
  });

  const abrirCerrarModalInsertar = () => {
    setModalInsertar(!modalInsertar);
  };

  const abrirCerrarModalEditar = () => {
    setModalEditar(!modalEditar);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setClienteSeleccionado({
      ...clienteSeleccionado,
      [name]: value,
    });
  };

  const seleccionarCliente = (cliente) => {
    setClienteEditar(cliente);
    abrirCerrarModalEditar();
  };

  const peticionGet = async () => {
    try {
      const response = await axios.get(baseUrl);
      setData(response.data);
    } catch (error) {
      console.error("Error al obtener datos:", error);
    }
  };

  const peticionPost = async () => {
    try {
      delete clienteSeleccionado.id;
      await axios.post(baseUrl, clienteSeleccionado);
      abrirCerrarModalInsertar();
      peticionGet();
    } catch (error) {
      console.error("Error al insertar cliente:", error);
    }
  };

  const peticionPut = async () => {
    try {
      await axios.put(`${baseUrl}/${clienteEditar.id}`, clienteEditar);
      abrirCerrarModalEditar();
      peticionGet();
    } catch (error) {
      console.error("Error al editar cliente:", error);
    }
  };

  const peticionDelete = async (id) => {
    try {
      await axios.delete(`${baseUrl}/${id}`);
      peticionGet();
    } catch (error) {
      console.error("Error al eliminar cliente:", error);
    }
  };

  useEffect(() => {
    peticionGet();
  }, []);

  return (
    <div>
      <button onClick={() => abrirCerrarModalInsertar()} className="btn btn-success">Insertar Cliente</button>
      <table className="styled-table">
        <thead>
          <tr>
            <th>Id_Cliente</th>
            <th>Nombre</th>
            <th>Dirección</th>
            <th>Telefono</th>
            <th>Opciones</th>
          </tr>
        </thead>
        <tbody>
          {data.map((cliente) => (
            <tr key={cliente.id}>
              <td>{cliente.id}</td>
              <td>{cliente.nombre}</td>
              <td>{cliente.direccion}</td>
              <td>{cliente.telefono}</td>
              <td>
                <button
                  className="btn btn-primary"
                  onClick={() => seleccionarCliente(cliente)}>Editar</button>{" "}
                  <button className="btn btn-danger" onClick={() => peticionDelete(cliente.id)}>Eliminar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <Modal isOpen={modalInsertar}>
        <ModalHeader>Insertar Nuevo Cliente</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nombre:</label>
            <input
              type="text"
              className="form-control"
              name="nombre"
              onChange={handleChange}
            />
            <br />
            <label>Direccion:</label>
            <input
              type="text"
              className="form-control"
              name="direccion"
              onChange={handleChange}
            />
            <br />
            <label>Telefono:</label>
            <input
              type="text"
              className="form-control"
              name="telefono"
              onChange={handleChange}
            />
          </div>
        </ModalBody>
        <ModalFooter>
          <button
            className="btn btn-primary"
            onClick={() => peticionPost()}
          >
            Insertar
          </button>{" "}
          <button
            className="btn btn-danger"
            onClick={() => abrirCerrarModalInsertar()}
          >
            Cancelar
          </button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar cliente</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nombre:</label>
            <input
              type="text"
              className="form-control"
              name="nombre"
              onChange={(e) =>
                setClienteEditar({
                  ...clienteEditar,
                  nombre: e.target.value,
                })
              }
              value={clienteEditar.nombre}
            />
            <br />
            <label>Direccion:</label>
            <input
              type="text"
              className="form-control"
              name="direccion"
              onChange={(e) =>
                setClienteEditar({
                  ...clienteEditar,
                  direccion: e.target.value,
                })
              }
              value={clienteEditar.direccion}
            />
            <br />
            <label>Telefono:</label>
            <input
              type="text"
              className="form-control"
              name="telefono"
              onChange={(e) =>
                setClienteEditar({
                  ...clienteEditar,
                  telefono: e.target.value,
                })
              }
              value={clienteEditar.telefono}
            />
          </div>
        </ModalBody>
        <ModalFooter>
          <button
            className="btn btn-primary"
            onClick={() => peticionPut()}
          >
            Guardar cambios
          </button>{" "}
          <button
            className="btn btn-danger"
            onClick={() => abrirCerrarModalEditar()}
          >
            Cancelar
          </button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default CrudCliente;
