using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringIsNullException : UnityException{
	public StringIsNullException(string message): base(message){
	}
}

public class CharacterStateOfNameAlreadyRegistered : UnityException{
    public CharacterStateOfNameAlreadyRegistered(string message) : base(message)
    {
    }
}