import React from 'react';
import { Navigate } from 'react-router-dom';

export const ProtectedRoute = ({children}) => {
    const token = localStorage.getItem('token');
    //const valid; verificación de que el token es válido
    /*
    proccess.env.KEY;
    const decode = jwt.verify(token, key, (err, decoded) => {
            if(err)
                return res.status(403).json({mensaje: 'Token inválido'})
            else
                next()
                //Si es necesario, se pueden establecer valores a req
                //Para enviar información al path solicitado
        })
    */
    if(token==="undefined" || token==null)// && valid)
        return <Navigate to="/" />

    return children;
}
/*
export const ProtectedRoute = ({ children }) => {
    const loggedIn = localStorage.getItem('token');
    if (!loggedIn) {
      window.location.replace('https://main.drvblo32vuwsq.amplifyapp.com/login');
    }
    return children;
  };
*/