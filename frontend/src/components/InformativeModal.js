import React from 'react'
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import './InformativeModal.css'

export const InformativeModal = ({show, onHide, title, message, icon}) => {
  return (
    <Modal show={show} onHide={onHide} size='sm'>
        <Modal.Header style={{fontWeight: 'bold'}}>{title}</Modal.Header>
        <Modal.Body><img src={icon} alt=''/>{message}</Modal.Body>
        <Modal.Footer className="ctr">
            <Button onClick={onHide}>Cerrar</Button>
        </Modal.Footer>
    </Modal>
  )
}
