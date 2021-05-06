import React, { useState } from  'react';
import App from '../App';
import UserForm from './UserForm';

const DisplayForm = (props) => {
    const { firstName, lastName, email, password, confirmPassword } = this.props.UserForm
    return(
        <div>
            <p>First Name: { firstName }</p>
            <p>First Name: { lastName }</p>
            <p>First Name: { email }</p>
            <p>First Name: { password }</p>
            <p>First Name: { confirmPassword }</p>
        </div>
    )

}
export default DisplayForm;