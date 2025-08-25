import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getPropertyDetails } from '../api/propertiesApi';
import "../assets/propertyDetails.css";

const PropertyDetails = () => {
    const { id } = useParams();
    const [property, setProperty] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedImage, setSelectedImage] = useState(null);

    useEffect(() => {
        const fetchProperty = async () => {
            try {
                const response = await getPropertyDetails(id);
                setProperty(response.data);

                if (response.data.images && response.data.images.length > 0) {
                    setSelectedImage(response.data.images[0].file);
                }

            } catch (err) {
                const errorMessage = err.response?.data?.message
                    || "The property could not be loaded.";
                setError(errorMessage);
            } finally {
                setLoading(false);
            }
        };

        fetchProperty();
    }, [id]);

    if (loading) {
        return (
            <div className="d-flex justify-content-center align-items-center py-5">
                <div className="spinner-border text-primary me-3" role="status">
                    <span className="visually-hidden">Loading...</span>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="alert alert-danger text-center my-5" role="alert">
                <i className="bi bi-exclamation-triangle-fill me-2"></i>
                Error: {error}
            </div>
        );
    }

    if (!property) {
        return (
            <div className="alert alert-warning text-center my-5" role="alert">
                <i className="bi bi-house-x-fill me-2"></i>
                Property not found.
            </div>
        );
    }

    const formatDate = (dateString) => {
        const options = { year: 'numeric', month: 'long', day: 'numeric' };
        return new Date(dateString).toLocaleDateString(undefined, options);
    };

    return (
        <div className="container my-4">
            {/* Property Info */}
            <div className="card mb-4 shadow-sm">
                <div className="card-body">
                    <h1 className="h4 card-title mb-3">{property.name}</h1>
                    <p className="text-muted">
                        <strong>Address:</strong> {property.address}
                    </p>
                    <p className="text-muted">
                        <strong>Price:</strong> ${property.price.toLocaleString()}
                    </p>
                    <p className="text-muted">
                        <strong>Internal Code:</strong> {property.codeInternal}
                    </p>
                    <p className="text-muted">
                        <strong>Year:</strong> {property.year}
                    </p>
                </div>
            </div>

            {/* Image Gallery */}
            <div className="card mb-4 shadow-sm">
                <div className="card-body">
                    <h2 className="h5 mb-3">Image Gallery</h2>

                    {property.images && property.images.length > 0 ? (
                        <div className="row">
                            {/* thumbnails */}
                            <div className="col-md-2 d-flex flex-column gap-2">
                                {property.images.map((img) => (
                                    <img
                                        key={img.idPropertyImage}
                                        src={img.file}
                                        alt="thumbnail"
                                        className="img-thumbnail gallery-thumb"
                                        onMouseEnter={() => setSelectedImage(img.file)}
                                    />
                                ))}
                            </div>

                            {/* main image with zoom */}
                            <div className="col-md-10">
                                <div className="gallery-main border rounded shadow-sm">
                                    <img
                                        src={selectedImage}
                                        alt="selected"
                                        className="img-fluid zoomable"
                                    />
                                </div>
                            </div>
                        </div>
                    ) : (
                        <p>No images available for this property.</p>
                    )}
                </div>
            </div>

            {/* Owner Info */}
            {property.owner && (
                <div className="card mb-4 shadow-sm">
                    <div className="card-body">
                        <h2 className="h5 mb-3">Owner Information</h2>
                        <div className="d-flex align-items-center gap-3">
                            <img
                                src={property.owner.photo}
                                alt={property.owner.name}
                                className="rounded-circle object-fit-cover"
                                style={{ width: "100px", height: "100px" }}
                            />
                            <div>
                                <p>
                                    <strong>Name:</strong> {property.owner.name}
                                </p>
                                <p>
                                    <strong>Address:</strong> {property.owner.address}
                                </p>
                                <p>
                                    <strong>Date of Birth:</strong>{" "}
                                    {formatDate(property.owner.birthday)}
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            )}

            {/* Sales History */}
            <div className="card mb-4 shadow-sm">
                <div className="card-body">
                    <h2 className="h5 mb-3">Sales History</h2>
                    {property.traces && property.traces.length > 0 ? (
                        <ul className="list-group">
                            {property.traces.map((trace) => (
                                <li key={trace.idPropertyTrace} className="list-group-item">
                                    <strong>{trace.name}</strong> - Sold on {formatDate(trace.dateSale)} for{" "}
                                    <span className="text-success fw-bold">
                                        ${trace.value.toLocaleString()}
                                    </span>{" "}
                                    (Tax: ${trace.tax.toLocaleString()})
                                </li>
                            ))}
                        </ul>
                    ) : (
                        <p className="text-muted">No sales history for this property.</p>
                    )}
                </div>
            </div>
        </div>
    );
};

export default PropertyDetails;