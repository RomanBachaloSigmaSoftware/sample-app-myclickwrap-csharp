import React from "react";
import PropTypes from "prop-types";

export const ClickWrap = ({ accountId, clickWrapId, userEmail, baseUrl }) => {
  return (
    <div className="form-group">
      {window.docuSignClick.Clickwrap.render(
        {
          environment: baseUrl,
          accountId: accountId,
          clickwrapId: clickWrapId,
          clientUserId: userEmail,
        },
        "#ds-clickWrap"
      )}
    </div>
  );
};

ClickWrap.propTypes = {
  accountId: PropTypes.string.isRequired,
  clickWrapId: PropTypes.string.isRequired,
  userEmail: PropTypes.string.isRequired,
  baseUrl: PropTypes.string.isRequired,
};
