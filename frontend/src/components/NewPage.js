import React, { useEffect } from 'react';
//import './NewPage.css';
import { useParams, useNavigate } from 'react-router-dom';

export const NewPage = () => {
  const { token } = useParams();
  const navigate = useNavigate();
  useEffect(() => {
    const url = `https://jgvuug8gmj.execute-api.us-east-1.amazonaws.com/decodeToken`;
    const uriVideogame = "https://0vnx9rbox9.execute-api.us-east-1.amazonaws.com/api/videogame/user";
    fetch(url, {
      mode: 'no-cors',
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        'x-access-token': token,
        'Access-Control-Allow-Origin': '*'
      },
      body: JSON.stringify({
        token: token,
      }),
    }).then((response) => response.json())
      .then((data) => {
        if (data.message === 'Token is valid') {
          localStorage.setItem('token', token);
          localStorage.setItem('userIdentificator', data.decoded.idUser);
          localStorage.setItem('username', data.decoded.username);
          localStorage.setItem('isAdmin', data.decoded.isAdmin);
          fetch(uriVideogame, { mode: 'no-cors',
                                headers: { 'Content-Type': 'application/json',
                                            'Access-Control-Allow-Origin': '*'}, 
                                        method: 'POST',
                                        body: JSON.stringify(
                                        { 
                                          userIdentificator: localStorage.getItem('userIdentificator'),
                                          username: localStorage.getItem('username')
                                        })
                                      });
          
          navigate('/home');
        } else {
          navigate('https://main.drvblo32vuwsq.amplifyapp.com/login');
        }
      });
      //console.log(res);
  }, [token, navigate]);

  return <div className='new-page'>Cargando...</div>;
};