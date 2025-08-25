import React from 'react';
import PropertyCard from './PropertyCard';

const PropertiesList = ({ properties }) => {
    if (!properties || properties.length === 0) {
        return <p class="text-primary">No properties found</p>;
    }

    return (
        <div className="row">
            {properties.map((property) => (
                <div className="col-md-4 mb-4" key={property.idProperty}>
                    <PropertyCard property={property} />
                </div>
            ))}
        </div>
    );
};

export default PropertiesList;
