﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAnimator : ThirdPersonMotor
{
    #region Variables                

    public const float walkSpeed = 0.5f;
    public const float runningSpeed = 1f;
    public const float sprintSpeed = 1.5f;

    #endregion

    public virtual void UpdateAnimator()
    {
        if (animator == null || !animator.enabled) return;


        animator.SetBool(vAnimatorParameters.IsStrafing, isStrafing); ;
        animator.SetBool(vAnimatorParameters.IsSprinting, isSprinting);
        animator.SetBool(vAnimatorParameters.IsGrounded, isGrounded);
        animator.SetFloat(vAnimatorParameters.GroundDistance, groundDistance);
        animator.SetBool(vAnimatorParameters.IsJumping, isJumping);
        animator.SetBool(vAnimatorParameters.IsAttacking, isAttacking);
        animator.SetBool(vAnimatorParameters.IsAiming, isAiming);
        animator.SetBool(vAnimatorParameters.IsSitting, isSitting);
        animator.SetFloat("rotate_hor", horDiffAngle, freeSpeed.animationSmooth, Time.deltaTime);
        animator.SetFloat("rotate_ver", verDiffAngle, freeSpeed.animationSmooth, Time.deltaTime);


        if (isStrafing)
        {
            animator.SetFloat(vAnimatorParameters.InputHorizontal, stopMove ? 0 : horizontalSpeed, strafeSpeed.animationSmooth, Time.deltaTime);
            animator.SetFloat(vAnimatorParameters.InputVertical, stopMove ? 0 : verticalSpeed, strafeSpeed.animationSmooth, Time.deltaTime);
        }
        else
        {
            animator.SetFloat(vAnimatorParameters.InputVertical, stopMove ? 0 : verticalSpeed, freeSpeed.animationSmooth, Time.deltaTime);
        }

        animator.SetFloat(vAnimatorParameters.InputMagnitude, stopMove ? 0f : inputMagnitude, isStrafing ? strafeSpeed.animationSmooth : freeSpeed.animationSmooth, Time.deltaTime);
    }

    public virtual void SetAnimatorMoveSpeed(vMovementSpeed speed)
    {
        Vector3 relativeInput = transform.InverseTransformDirection(moveDirection);
        verticalSpeed = relativeInput.z;
        horizontalSpeed = relativeInput.x;

        var newInput = new Vector2(verticalSpeed, horizontalSpeed);

        if (speed.walkByDefault)
        {

            inputMagnitude = Mathf.Clamp(isSprinting ? newInput.magnitude+0.5f : newInput.magnitude/2.0f, 0, isSprinting ? sprintSpeed : walkSpeed);
        }
        else
        {
            inputMagnitude = Mathf.Clamp(isSprinting ? newInput.magnitude + 0.5f : newInput.magnitude, 0, isSprinting ? sprintSpeed : runningSpeed);
        }
    }
}
public static partial class vAnimatorParameters
{
    public static int InputHorizontal = Animator.StringToHash("InputHorizontal");
    public static int InputVertical = Animator.StringToHash("InputVertical");
    public static int InputMagnitude = Animator.StringToHash("InputMagnitude");
    public static int IsGrounded = Animator.StringToHash("IsGrounded");
    public static int IsStrafing = Animator.StringToHash("IsStrafing");
    public static int IsSprinting = Animator.StringToHash("IsSprinting");
    public static int IsJumping = Animator.StringToHash("IsJumping");
    public static int IsAttacking = Animator.StringToHash("IsAttacking");
    public static int IsAiming = Animator.StringToHash("IsAiming");
    public static int IsSitting = Animator.StringToHash("IsSitting");
    public static int GroundDistance = Animator.StringToHash("GroundDistance");
}

