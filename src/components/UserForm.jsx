import { ifError } from 'assert';
import React, { useState } from  'react';
    
    
const UserForm = (props) => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");  
    const [confirmPassword, setConfirm] = useState("");  

    const [firstNameError, setFirstNameError] = useState("");
    const [lastNameError, setLastNameError] = useState("");
    const [emailError, setEmailError] = useState("");
    const [passwordError, setPasswordError] = useState("");
    const [confirmError, setConfirmError] = useState("");

    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    
    const createUser = (e) => {
        e.preventDefault();
        const newUser = { firstName, lastName, email, password, confirmPassword };
        console.log("Welcome", newUser);
    };

    const handleFirstName = (e) => {
        setFirstName(e.target.value);

        if(e.target.value.length < 1) {
            setFirstNameError("Name is required!");
        } 
        else if(e.target.value.length < 2) {
            setFirstNameError("Name must be 2 characters or longer!");
        } 
        else if(e.target.value.length >= 3) {
            setFirstNameError("");
        }
        
    }
    const handleLastName = (e) => {
        setLastName(e.target.value);

        if(e.target.value.length < 1) {
            setLastNameError("Name is required!");
        } 
        else if(e.target.value.length < 2) {
            setLastNameError("Name must be 2 characters or longer.");
        } 
        else if(e.target.value.length >= 3) {
            setLastNameError("");
        }
        
    }
    const handleEmail = (e) => {
        setEmail(e.target.value);

        if(e.target.value.length < 1) {
            setEmailError("Name is required!");
        } 
        else if(e.target.value.length < 5) {
            setEmailError("Email must be 5 characters or longer!");
        } 
        // else if(e.target.value.length >= 3) {
        //     setEmailError("");
        // }
        else if (re.test(e.target.value)){
                setEmailError("It shoud read aol.com.")
        } 

    }

    const handlePassword = (e) => {
        setPassword(e.target.value);

        if(e.target.value.length < 1) {
            setPasswordError("Name is required!");
        } 
        else if(e.target.value.length < 8) {
            setPasswordError("Name must be 8 characters or longer!");
        } 
        else if(e.target.value.length >= 8) {
            setPasswordError("");
        } 

    }

    const handleConfirm = (e) => {
        setConfirm(e.target.value);

        if(e.target.value.length < 1) {
            setConfirmError("Name is required!");
        } 
        else if(e.target.value.length < 8) {
            setConfirmError("Name must be 8 characters or longer.");
        } 
        else if(e.target.value.length !== password.length){
            setConfirmError("");
        }

        else if(e.target.value.length === password.length) {
            if (password !== e.target.value){
                setConfirmError("Passwords must match.")
            }
        }

        else if(e.target.value.length > password.length) {
            setConfirmError("Passwords must match.")
        }
        
    };


    
    return(
        <>
            <form onSubmit={ createUser }>
                <div class = "divbox">
                    <label>First name: </label> 
                    <input type="text" onChange={ handleFirstName } />
                    {   
                        firstNameError ?
                        <p> {firstNameError}</p> : ''
                    }
                </div>
                <div class = "divbox">
                    <label>Last name: </label> 
                    <input type="text" onChange={ handleLastName } />
                    {   
                        lastNameError ?
                        <p> {lastNameError}</p> : ''
                    }
                 </div>
                <div class = "divbox">
                    <label>Email Address: </label> 
                    <input type="text" onChange={ handleEmail } />
                    {   
                        emailError ?
                        <p> {emailError}</p> : ''
                    }
                </div>
                <div class = "divbox">
                    <label>Password: </label>
                    <input type="text" onChange={ handlePassword } />
                    {   
                        passwordError ?
                        <p> {passwordError}</p> : ''
                    }
                </div>
                <div class = "divbox">
                    <label>Confirm Password: </label>
                    <input type="text" onChange={ handleConfirm } />
                    {   
                        confirmError ?
                        <p> {confirmError}</p> : ''
                    }
                </div>
                <input type="submit" value="Create User" />
            </form>
            {/* <div>
            <p>First Name: { firstName }</p>
            <p>Last Name: { lastName }</p>
            <p>Email: { email }</p>
            <p>Password: { password }</p>
            <p>Confirm Password: { confirmPassword }</p>
        </div> */}
        </>
        
    );
    
    
};
    
export default UserForm;