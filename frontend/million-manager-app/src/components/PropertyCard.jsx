import React from 'react';
import { Link } from 'react-router-dom';
import '../assets/propertyCard.css';

const PropertyCard = ({ property }) => {
    return (
        <div className="card h-100 shadow-sm" style={{ borderRadius: '12px' }}>
            <img src={property.imageUrl} className="card-img-top" alt={property.name} style={{ height: '220px', objectFit: 'cover' }} />
            <div className="card-body">
                <h5 className="card-title">{property.name}</h5>
                <p className="card-text">
                    <strong>Address:</strong> {property.addressProperty}</p>
                <p className="card-text">
                    <strong>Price:</strong> ${property.priceProperty.toLocaleString()}</p>
                <Link to={`/property/${property.idProperty}`} className="vista-button">Details</Link>
            </div>
        </div>
    );
};


export default PropertyCard;