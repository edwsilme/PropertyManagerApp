import { Routes, Route, Link } from 'react-router-dom';
import HomePage from './pages/index.jsx';
import PropertyDetails from './pages/PropertyDetails.jsx';

function App() {
    return (
        <div>
            <header className="bg-primary text-white p-3 mb-4">
                <div className="container d-flex justify-content-between align-items-center">
                    <h1 className="h3 m-0">Million Manager App</h1>
                    <nav>
                        <Link to="/" className="text-white text-decoration-none">Home Page</Link>
                    </nav>
                </div>
            </header>

            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/property/:id" element={<PropertyDetails />} />
            </Routes>
        </div>
    );
}

export default App;
