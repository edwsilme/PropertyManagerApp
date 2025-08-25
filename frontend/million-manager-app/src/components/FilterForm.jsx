import React, { useState } from 'react';
import "../assets/form.css";

const FilterForm = ({ onFilterChange }) => {
    const [name, setName] = useState('');
    const [address, setAddress] = useState('');
    const [minPrice, setMinPrice] = useState('');
    const [maxPrice, setMaxPrice] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        onFilterChange({
            name,
            address,
            minPrice: minPrice ? parseFloat(minPrice) : null,
            maxPrice: maxPrice ? parseFloat(maxPrice) : null
        });
    };

    return (
        <form className="filter-form row g-3" onSubmit={handleSubmit}>
            <div className="col-md-3">
                <input
                    className="form-control"
                    type="text"
                    placeholder="Property Name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
            </div>
            <div className="col-md-3">
                <input
                    className="form-control"
                    type="text"
                    placeholder="Address"
                    value={address}
                    onChange={(e) => setAddress(e.target.value)}
                />
            </div>
            <div className="col-md-2">
                <input
                    className="form-control"
                    type="number"
                    placeholder="Min Price"
                    value={minPrice}
                    onChange={(e) => setMinPrice(e.target.value)}
                />
            </div>
            <div className="col-md-2">
                <input
                    className="form-control"
                    type="number"
                    placeholder="Max Price"
                    value={maxPrice}
                    onChange={(e) => setMaxPrice(e.target.value)}
                />
            </div>
            <div className="col-md-2 d-grid">
                <button type="submit" className="button2">Search</button>
            </div>
        </form>
    );
};

export default FilterForm;
