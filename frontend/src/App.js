import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Login } from './components/Login';
import { Layout } from './components/Layout';
import { ProtectedRoute } from './components/ProtectedRoute';
import { Friends } from './components/Friends';
import { Stats } from './components/Stats';
import { NewPage } from './components/NewPage';
import { PasswordRecovery } from './components/PasswordRecovery';
import './App.css';
import UnityGame from './components/UnityGame';

function App() {
  return (
    <BrowserRouter>
      <Routes>
      <Route path="/" element={<Login />}></Route>
        <Route path="/home" element={
          <ProtectedRoute>
        <Layout />
        </ProtectedRoute>
        }>
          <Route index element={
                <ProtectedRoute>
                  <div>
                   <UnityGame></UnityGame>
                  </div>
                </ProtectedRoute>}></Route>
          <Route path="friends" element={<ProtectedRoute><Friends /></ProtectedRoute>}></Route>
          <Route path="stats" element={<ProtectedRoute><Stats /></ProtectedRoute>}></Route>
        </Route>
      </Routes>
    </BrowserRouter>    
  );
}

export default App;
