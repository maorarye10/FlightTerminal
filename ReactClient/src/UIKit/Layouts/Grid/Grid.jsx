import './Grid.css';

export const Grid = ({ children, addClass }) => {
    return (
        <div className={`Grid ${addClass || ''}`}>
            {children}
        </div>
    )
}

export const GridMain = (props) => {
    return <Grid {...props} addClass="main" />
}

export const GridSideBar = (props) => {
    return <Grid {...props} addClass="sidebar" />
}