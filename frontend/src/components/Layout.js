import React from 'react'
import Navbar from 'react-bootstrap/Navbar'
import Nav from 'react-bootstrap/Nav'
import Container from 'react-bootstrap/Container'
import { Outlet, useNavigate } from 'react-router-dom'
import UnityGame from './UnityGame'
import gameIcon from '../assets/games.png'


const navbarStyle = {
  display: 'flex',
  flexDirection: 'row',
  justifyContent: 'center',
  alignItems: 'center',
  width: 'auto',
  height: '175px',
  backgroundImage: 'url(https://res.cloudinary.com/dt5oxu5y7/image/upload/v1681950278/fondo-degradado-1-1_birdnz.png)',
  backgroundSize: 'cover',
  borderRadius: '0 0 25px 25px',
  padding: '0 20px',
  position: 'relative',
}

const newImageStyle = {
  position: 'absolute',
  bottom: '-20px', 
  right: '400px', 
  width: '150px', 
  height: 'auto',
}

const topLeftImageStyle = {
  position: 'absolute',
  top: '0', 
  left: '0', 
  width: '175px',
  height: 'auto',
};

const logoutIconStyle = {
  position: 'absolute',
  top: '10px', 
  right: '10px', 
  width: '30px', 
  height: 'auto',
  cursor: 'pointer',
};

const friendsIconStyle = {
  position: 'absolute',
  top: '10px', 
  right: '50px',
  width: '30px', 
  height: 'auto',
  cursor: 'pointer',
};

const statsIconStyle = {
  position: 'absolute',
  top: '10px',
  right: '90px',
  width: '30px',
  height: 'auto',
  cursor: 'pointer',
}

const gameIconStyle = {
  position: 'absolute',
  top: '10px',
  right: '130px',
  width: '30px',
  height: 'auto',
  cursor: 'pointer',
}

const logoStyle = {
  width: '250px',
  height: 'auto',
}

const containerStyle = {
  width: '100%',
  padding: '0',
  overflowX: 'hidden',
}


export const Layout = () => {
  const navigate = useNavigate();

  const redirectToHome = () => {
    navigate('/home');
  };

  const redirectToFriends = () => {
    navigate('/home/friends');
  };

  const redirectToStats = () => {
    navigate('/home/stats');
  }

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userIdentificator");
    localStorage.removeItem("username");
    window.location.replace('https://main.drvblo32vuwsq.amplifyapp.com/login');
  }

  return (
    <div style={containerStyle}>
      <div style={navbarStyle}>
        <div onClick={redirectToHome}>
          <img
            src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682093597/mocks-display-aulify-17-1-1_gaimvc.png"
            alt="Logo"
            style={logoStyle}
          />
        </div>
        <img
          src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1681951983/mocks-display-aulify-18-1_kt8wwq.png"
          alt=""
          style={newImageStyle}
        />
        <img
          src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682089978/mocks-display-aulify-22-1_jqpavz.png"
          alt=""
          style={topLeftImageStyle}
        />
        <div onClick={(e) => { e.stopPropagation(); redirectToFriends(); }}>
          <img
            src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682093554/group-solid-60_vv9lm9.png"
            alt="Friends"
            style={statsIconStyle}
          />
        </div>
        <div onClick={(e) => { e.stopPropagation(); logout(); }}>
          <img
            src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682093547/log-out-circle-regular-60_isyqny.png"
            alt="Logout"
            style={logoutIconStyle}
          />
        </div>
        <div onClick={(e) => { e.stopPropagation(); redirectToStats(); }}>
          <img 
            src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682639629/bar-chart-alt-2-solid-60_zujmwh.png"
            alt="Stats"
            style={friendsIconStyle}
          />
        </div>
        <div onClick={(e) => { e.stopPropagation(); redirectToHome(); }}>
          <img 
            src={gameIcon}
            alt="Videogame"
            style={gameIconStyle}
          />
        </div>
      </div> 
      <section>
        <Outlet/>
      </section>
    </div>
  )
}
