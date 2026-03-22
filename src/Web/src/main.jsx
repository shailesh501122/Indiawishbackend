import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './styles.css';
import { HomePage } from './pages/HomePage';
import { ListingsPage } from './pages/ListingsPage';
import { PropertyDetailPage } from './pages/PropertyDetailPage';
import { AuthPage } from './pages/AuthPage';
import { DashboardPage } from './pages/DashboardPage';

const App = () => (
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/listings" element={<ListingsPage />} />
      <Route path="/properties/:slug" element={<PropertyDetailPage />} />
      <Route path="/auth" element={<AuthPage />} />
      <Route path="/dashboard" element={<DashboardPage />} />
    </Routes>
  </BrowserRouter>
);

ReactDOM.createRoot(document.getElementById('root')).render(<App />);
