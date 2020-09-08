import React from "react";
import { Spinner } from "react-bootstrap";

export const Loader = () => (
  <div className="spinner">
    <Spinner animation="border" role="status" />
  </div>
);
