export const Icon = ({ i, onClick }) => {
    return (
        <span className="material-symbols-outlined" onClick={onClick}>
            {i}
        </span>
    )
}