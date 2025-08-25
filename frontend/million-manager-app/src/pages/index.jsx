import React, { useState, useEffect } from 'react';
import { getProperties } from '../api/propertiesApi';
import PropertiesList from '../components/PropertiesList';
import FilterForm from '../components/FilterForm';
import "../assets/HomePage.css";

const HomePage = () => {
    const [properties, setProperties] = useState([]);
    const [filters, setFilters] = useState({ name: '', address: '', minPrice: null, maxPrice: null });

    useEffect(() => {
        const fetchProperties = async () => {
            try {
                const response = await getProperties(filters);
                setProperties(response.data);
            } catch (error) {
                console.error("Error al obtener propiedades:", error);
            }
        };

        fetchProperties();
    }, [filters]);

    const handleFilterChange = (newFilters) => {
        setFilters(newFilters);
    };

    return (
        <div className="container my-5">
            {/* Header */}
            <div className="text-center mb-5">
                <h1>Real Estate</h1>
                <p className="lead text-muted">
                    Find your perfect property
                </p>
            </div>
            <div className="">
                <div className="card-body">
                    <FilterForm onFilterChange={handleFilterChange} />
                </div>
            </div>
            <div>
                {properties.length > 0 ? (
                    <PropertiesList properties={properties} />
                ) : (
                    <div className="alert alert-info text-center">
                        No properties found with the selected filters.
                    </div>
                )}
            </div>
        </div>
    );
};

export default HomePage;