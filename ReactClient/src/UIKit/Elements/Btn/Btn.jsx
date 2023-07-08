import { forwardRef, useEffect } from 'react';
import { Between } from '../../Layouts/Line/Line';
import { Icon } from '../Icon/Icon';
import './Btn.css';

export const Btn = forwardRef(({ children, onClick, i }, ref) => {

    useEffect(() => {
        console.log('render BTN');
    })

    return (
        <button className='Btn' onClick={onClick} ref={ref}>
            <Between>
                {i && <Icon i={i} />}
                {children}
            </Between>
        </button>
    )
})